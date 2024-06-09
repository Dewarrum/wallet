<script lang="ts">
	import ProfileSelector from '$lib/profiles/ProfileSelector.svelte';
	import AddTransactionButton from '$lib/transactions/AddTransactionButton.svelte';
	import TransactionList from '$lib/transactions/TransactionList.svelte';

	let profileId: string | null;
</script>

<div class="container">
	<div class="mb-2 flex space-x-4">
		<h2 class="h2">Profiles</h2>

		<div>
			<ProfileSelector bind:profileId />
		</div>

		<span class="flex-grow"></span>

		{#if profileId}
			<div>
				<AddTransactionButton {profileId} />
			</div>
		{/if}
	</div>

	<h3 class="h3 mb-2">Transactions</h3>
	{#if profileId}
		<TransactionList {profileId} />
	{/if}
</div>
