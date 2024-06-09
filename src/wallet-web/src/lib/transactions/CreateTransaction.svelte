<script lang="ts">
	import CategorySelector from '$lib/categories/CategorySelector.svelte';
	import type { Category } from '$lib/categories/models';
	import { getCategories } from '$lib/categories/queries';
	import { getProfile } from '$lib/profiles/queries';
	import { addTransaction } from '$lib/transactions/mutations';
	import { PaymentMethod } from '$lib/utils/payment-method';
	import { TransactionType } from '$lib/utils/transaction-type';
	import {
		ProgressBar,
		ProgressRadial,
		RadioGroup,
		RadioItem,
		SlideToggle,
		getModalStore
	} from '@skeletonlabs/skeleton';
	import { useQueryClient } from '@tanstack/svelte-query';

	const modalStore = getModalStore();
	let profileId = $modalStore[0].meta.profileId;

	const profileQuery = getProfile(profileId);
	const categoryQuery = getCategories();
	const queryClient = useQueryClient();
	const addTransactionMutation = addTransaction();

	let amount = '0';
	let category: Category;
	let transactionType = TransactionType.Expense;
	let paymentMethod = PaymentMethod.Cash.value;
	let isRecurring = false;
	let description = '';
	let merchant = '';

	function onSubmit() {
		$addTransactionMutation.mutate(
			{
				profileId,
				amount: Number(amount),
				categoryId: category.id,
				transactionType,
				paymentMethod: paymentMethod,
				isRecurring,
				description,
				merchant
			},
			{
				onSuccess: () => {
					queryClient.invalidateQueries({
						queryKey: ['profile', profileId, 'transactions']
					});
					modalStore.close();
				}
			}
		);
	}
</script>

<div class="card w-modal space-y-4 p-4 shadow-xl">
	<header class="text-2xl font-bold">Add transaction</header>
	<article>Complete the form below and then press submit.</article>
	<form
		class="modal-form space-y-4 border border-surface-500 p-4 rounded-container-token"
		on:submit|preventDefault={onSubmit}
	>
		{#if $profileQuery.isLoading || $categoryQuery.isLoading}
			<ProgressBar />
		{:else if $profileQuery.data && $categoryQuery.data}
			<label class="label">
				<span>Amount</span>
				<div class="input-group input-group-divider grid-cols-[auto_1fr_auto]">
					<div class="input-group-shim">{$profileQuery.data.currency}</div>
					<input type="text" placeholder="Amount" bind:value={amount} required />
				</div>
			</label>

			<div class="label">
				<span>Category</span>
				<CategorySelector bind:category />
			</div>

			<div class="label">
				<span>Transaction type</span>
				<RadioGroup
					class="input"
					active="variant-filled-primary"
					hover="hover:variant-soft-primary"
				>
					<RadioItem
						bind:group={transactionType}
						value={TransactionType.Expense}
						name="transactionType">Expense</RadioItem
					>
					<RadioItem
						bind:group={transactionType}
						value={TransactionType.Income}
						name="transactionType">Income</RadioItem
					>
				</RadioGroup>
			</div>

			<label class="label">
				<span>Payment method</span>
				<select class="select" bind:value={paymentMethod}>
					{#each PaymentMethod.All as paymentMethod}
						<option value={paymentMethod.value}>{paymentMethod.name}</option>
					{/each}
				</select>
			</label>

			<SlideToggle name="isRecurring" bind:checked={isRecurring}>Is recurring?</SlideToggle>

			<label class="label">
				<span>Description</span>
				<input class="input" type="text" placeholder="Description" bind:value={description} />
			</label>

			<label class="label">
				<span>Merchant</span>
				<input
					type="text"
					class="input"
					placeholder="Name of shop or seller"
					bind:value={merchant}
				/>
			</label>

			<div class="label">
				<button type="submit" class="variant-filled btn">
					{#if $addTransactionMutation.isPending}
						<ProgressRadial />
					{/if}
					<span>Submit</span>
				</button>
			</div>
		{/if}
	</form>
</div>
