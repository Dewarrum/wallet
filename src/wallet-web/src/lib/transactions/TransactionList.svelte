<script lang="ts">
	import { getProfile } from '$lib/profiles/queries';
	import { ProgressBar } from '@skeletonlabs/skeleton';
	import { getTransactionsForProfile } from './queries';
	import type { Profile } from '$lib/profiles/models';
	import type { CreateQueryResult } from '@tanstack/svelte-query';
	import type { Transaction } from './models';
	import type { PageResponse } from '$lib/utils/page-response';

	export let profileId: string;

	let profileQuery: CreateQueryResult<Profile, Error>;
	let transactionQuery: CreateQueryResult<PageResponse<Transaction>, Error>;

	$: {
		profileQuery = getProfile(profileId);
		transactionQuery = getTransactionsForProfile(profileId);
	}
</script>

{#if $profileQuery.isLoading || $transactionQuery.isLoading}
	<ProgressBar />
{:else if $profileQuery.data && $transactionQuery.data}
	{#if $transactionQuery.data.items.length}
		{#each $transactionQuery.data.items as transaction}
			<div class="card mb-2">
				<header class="card-header">{$profileQuery.data.currency} {transaction.amount}</header>
				<footer class="card-footer">{transaction.createdAt}</footer>
			</div>
		{/each}
	{:else}
		<h4 class="h4">There're no transactions</h4>
	{/if}
{/if}
